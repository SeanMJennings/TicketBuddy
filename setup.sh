#!/bin/bash
set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

if [ ! -f /etc/os-release ] || ! grep -qE "^ID=(ubuntu|debian)$" /etc/os-release; then
    echo "This script only supports Ubuntu and Debian"
    exit 1
fi

. /etc/os-release

export DOTNET_EnableWriteXorExecute=0
if ! grep -q "DOTNET_EnableWriteXorExecute" ~/.bashrc 2>/dev/null; then
    echo 'export DOTNET_EnableWriteXorExecute=0' >> ~/.bashrc
fi

echo "Installing prerequisites..."
sudo apt-get update
sudo apt-get install -y curl ca-certificates libnss3-tools

has_dotnet10=false
if command -v dotnet >/dev/null 2>&1; then
    if dotnet --list-sdks 2>/dev/null | grep -q "^10\."; then
        has_dotnet10=true
    fi
fi

if [ "$has_dotnet10" = false ]; then
    echo "Installing .NET 10 SDK..."
    curl -fsSL https://packages.microsoft.com/config/$ID/$VERSION_ID/packages-microsoft-prod.deb -o packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb
    sudo apt-get update
    sudo apt-get install -y dotnet-sdk-10.0
fi

if ! command -v docker >/dev/null 2>&1; then
    echo "Installing Docker..."
    sudo apt-get update
    sudo apt-get install -y ca-certificates curl gnupg
    sudo install -m 0755 -d /etc/apt/keyrings
    curl -fsSL https://download.docker.com/linux/$ID/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
    sudo chmod a+r /etc/apt/keyrings/docker.gpg
    echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/$ID $VERSION_CODENAME stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
    sudo apt-get update
    sudo apt-get install -y docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
    sudo usermod -aG docker "$USER"
fi

if ! command -v node >/dev/null 2>&1; then
    echo "Installing Node.js LTS..."
    curl -fsSL https://deb.nodesource.com/setup_lts.x | sudo -E bash -
    sudo apt-get install -y nodejs
fi

hash -r

if ! command -v aspire >/dev/null 2>&1; then
    echo "Installing .NET Aspire workload..."
    sudo dotnet workload install aspire
fi

if ! command -v gh >/dev/null 2>&1; then
    echo "Installing Github ..."
    sudo apt-get install -y gh
fi

echo "Configuring GitHub NuGet feed..."
read -p "Enter GitHub username: " githubUsername

if ! gh auth status --hostname github.com 2>/dev/null | grep -q "read:packages"; then
    gh auth login --scopes read:packages --git-protocol ssh --hostname github.com --web
fi
token=$(gh auth token)

if [ -n "$token" ]; then
    dotnet nuget remove source github 2>/dev/null || true
    dotnet nuget add source "https://nuget.pkg.github.com/SeanMJennings/index.json" \
        --name "github" \
        --username "$githubUsername" \
        --password "$token" \
        --store-password-in-clear-text
fi

echo "Setting up HTTPS development certificates..."
export SSL_CERT_DIR="$HOME/.aspnet/dev-certs/trust:/etc/ssl/certs"
echo 'export SSL_CERT_DIR="$HOME/.aspnet/dev-certs/trust:/etc/ssl/certs"' >> ~/.bashrc
dotnet dev-certs https --clean
dotnet dev-certs https --export-path ~/aspnetcore-dev-cert.crt --format PEM --no-password
sudo cp ~/aspnetcore-dev-cert.crt /usr/local/share/ca-certificates/aspnetcore-dev-cert.crt
sudo update-ca-certificates
dotnet dev-certs https --trust

echo "You may still need to navigate to localhost:5001 in browser when running and allow"

echo "Installing UI dependencies..."
cd "$SCRIPT_DIR/UI"
npm install
echo "Installing Playwright browsers..."
sudo npx playwright install-deps
npx playwright install
cd "$SCRIPT_DIR"

echo "Restoring .NET packages..."
dotnet restore "$SCRIPT_DIR/ModularMonolith/TicketBuddy.slnx"

echo "Setting permissions on Kubernetes scripts..."
chmod +x "$SCRIPT_DIR/k8s/provision.sh"
chmod +x "$SCRIPT_DIR/k8s/teardown.sh"

echo ""
echo "Setup complete!"
echo "Note: You may need to log out and back in for Docker group membership to take effect."
echo "Run with: cd ModularMonolith/LocalHosting/Host.Aspire && dotnet run"
