# Installing Make

This guide will help you install `make` on your system.

## Why Make?

Make is a build automation tool that simplifies running common commands. Instead of typing long commands like:

```bash
dotnet run --project BackendTechnicalAssetsManagement/BackendTechnicalAssetsManagement.csproj --launch-profile http
```

You can simply type:
```bash
make run
```

## Installation

### Windows

#### Method 1: Using winget (Recommended)

1. Open PowerShell as Administrator
2. Run:
   ```powershell
   winget install GnuWin32.Make
   ```
3. **Restart your terminal** for PATH changes to take effect
4. Verify installation:
   ```bash
   make --version
   ```

#### Method 2: Using Chocolatey

If you have Chocolatey installed:
```powershell
choco install make
```

#### Method 3: Using Scoop

If you have Scoop installed:
```powershell
scoop install make
```

#### Method 4: Git Bash

If you have Git for Windows installed, `make` is already available in Git Bash. Just open Git Bash instead of PowerShell.

### Linux

#### Ubuntu/Debian
```bash
sudo apt-get update
sudo apt-get install build-essential
```

#### Fedora/RHEL/CentOS
```bash
sudo dnf install make
```

#### Arch Linux
```bash
sudo pacman -S make
```

### macOS

#### Using Homebrew
```bash
brew install make
```

#### Using Xcode Command Line Tools
```bash
xcode-select --install
```

## Verification

After installation, verify `make` is working:

```bash
make --version
```

You should see output like:
```
GNU Make 3.81
...
```

## Troubleshooting

### Windows: "make: command not found"

**Solution 1:** Restart your terminal after installation.

**Solution 2:** Manually add to PATH:
1. Search for "Environment Variables" in Windows
2. Edit "Path" in System Variables
3. Add: `C:\Program Files (x86)\GnuWin32\bin`
4. Restart terminal

**Solution 3:** Use Git Bash instead of PowerShell/CMD.

### Linux/Mac: "make: command not found"

Install the build tools for your distribution (see installation instructions above).

## Next Steps

Once `make` is installed:

1. Navigate to your project directory
2. Run `make help` to see all available commands
3. Run `make run` to start the application

See [QUICK_START.md](QUICK_START.md) for all available commands.
