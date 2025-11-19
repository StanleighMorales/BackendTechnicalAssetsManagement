# Makefile for Backend Technical Assets Management

# Variables
PROJECT_DIR = BackendTechnicalAssetsManagement
PROJECT_FILE = $(PROJECT_DIR)/BackendTechnicalAssetsManagement.csproj
TEST_DIR = BackendTechnicalAssetsManagementTest
TEST_FILE = $(TEST_DIR)/BackendTechnicalAssetsManagementTest.csproj

# Default target
.DEFAULT_GOAL := help

# Help target
.PHONY: help
help:
	@echo Available commands:
	@echo   make run          - Run the application with HTTP profile
	@echo   make dev          - Run the application in development mode
	@echo   make build        - Build the project
	@echo   make clean        - Clean build artifacts
	@echo   make restore      - Restore NuGet packages
	@echo   make test         - Run all tests
	@echo   make test-watch   - Run tests in watch mode
	@echo   make migrate      - Apply database migrations
	@echo   make migration    - Create a new migration (use NAME=MigrationName)
	@echo   make db-update    - Update database to latest migration
	@echo   make db-drop      - Drop the database

# Run the application with HTTP profile
.PHONY: run
run:
	dotnet run --project $(PROJECT_FILE) --launch-profile http

# Run in development mode
.PHONY: dev
dev:
	dotnet watch run --project $(PROJECT_FILE) --launch-profile http

# Build the project
.PHONY: build
build:
	dotnet build $(PROJECT_FILE)

# Clean build artifacts
.PHONY: clean
clean:
	dotnet clean $(PROJECT_FILE)
	dotnet clean $(TEST_FILE)

# Restore NuGet packages
.PHONY: restore
restore:
	dotnet restore $(PROJECT_FILE)
	dotnet restore $(TEST_FILE)

# Run tests
.PHONY: test
test:
	dotnet test $(TEST_FILE)

# Run tests in watch mode
.PHONY: test-watch
test-watch:
	dotnet watch test $(TEST_FILE)

# Run tests with coverage
.PHONY: test-coverage
test-coverage:
	dotnet test $(TEST_FILE) --collect:"XPlat Code Coverage"

# Create a new migration
.PHONY: migration
migration:
ifndef NAME
	@echo Error: Please specify migration name with NAME=YourMigrationName
	@exit 1
endif
	dotnet ef migrations add $(NAME) --project $(PROJECT_FILE)

# Apply migrations
.PHONY: migrate
migrate:
	dotnet ef database update --project $(PROJECT_FILE)

# Update database
.PHONY: db-update
db-update:
	dotnet ef database update --project $(PROJECT_FILE)

# Drop database
.PHONY: db-drop
db-drop:
	dotnet ef database drop --project $(PROJECT_FILE) --force

# Format code
.PHONY: format
format:
	dotnet format $(PROJECT_FILE)

# List all migrations
.PHONY: migrations-list
migrations-list:
	dotnet ef migrations list --project $(PROJECT_FILE)

# Install/update tools
.PHONY: tools
tools:
	dotnet tool restore
