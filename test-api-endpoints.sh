#!/bin/bash

# API Endpoint Testing Script (Bash version)
# Tests all endpoints for 404, 400, 500 errors
# Usage: ./test-api-endpoints.sh [BASE_URL] [TOKEN]

BASE_URL="${1:-https://localhost:7001}"
TOKEN="${2:-}"

# Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
MAGENTA='\033[0;35m'
NC='\033[0m' # No Color

# Counters
TOTAL_TESTS=0
PASSED_TESTS=0
FAILED_TESTS=0
SKIPPED_TESTS=0

# Results file
TIMESTAMP=$(date +"%Y%m%d_%H%M%S")
RESULTS_FILE="api-test-results_${TIMESTAMP}.txt"

echo -e "${MAGENTA}========================================${NC}"
echo -e "${MAGENTA}API ENDPOINT TESTING${NC}"
echo -e "${MAGENTA}========================================${NC}"
echo "Base URL: $BASE_URL"
echo "Auth Token: $([ -n "$TOKEN" ] && echo 'Provided' || echo 'Not provided')"
echo -e "${MAGENTA}========================================${NC}\n"

# Function to test endpoint
test_endpoint() {
    local method=$1
    local endpoint=$2
    local description=$3
    local requires_auth=$4
    local expected_codes=$5
    
    TOTAL_TESTS=$((TOTAL_TESTS + 1))
    local url="${BASE_URL}${endpoint}"
    
    echo -e "\n${CYAN}[$TOTAL_TESTS] Testing: $method $endpoint${NC}"
    echo -e "    Description: $description"
    
    # Skip if auth required but no token
    if [ "$requires_auth" = "true" ] && [ -z "$TOKEN" ]; then
        echo -e "    ${YELLOW}SKIPPED - Requires authentication token${NC}"
        SKIPPED_TESTS=$((SKIPPED_TESTS + 1))
        echo "$method|$endpoint|SKIPPED|N/A|Requires authentication" >> "$RESULTS_FILE"
        return
    fi
    
    # Prepare curl command
    local curl_cmd="curl -s -w '\n%{http_code}' -X $method"
    
    if [ "$requires_auth" = "true" ] && [ -n "$TOKEN" ]; then
        curl_cmd="$curl_cmd -H 'Authorization: Bearer $TOKEN'"
    fi
    
    curl_cmd="$curl_cmd -H 'Content-Type: application/json' -k '$url'"
    
    # Execute request
    local response=$(eval $curl_cmd 2>&1)
    local status_code=$(echo "$response" | tail -n1)
    
    # Check if status code is in expected codes
    if echo "$expected_codes" | grep -q "$status_code"; then
        echo -e "    ${GREEN}✓ PASSED - Status: $status_code${NC}"
        PASSED_TESTS=$((PASSED_TESTS + 1))
        echo "$method|$endpoint|PASSED|$status_code|Success" >> "$RESULTS_FILE"
    elif [ "$status_code" = "404" ]; then
        echo -e "    ${RED}✗ FAILED - 404 Not Found${NC}"
        FAILED_TESTS=$((FAILED_TESTS + 1))
        echo "$method|$endpoint|FAILED|404|Endpoint not found" >> "$RESULTS_FILE"
    elif [ "$status_code" -ge 500 ] 2>/dev/null; then
        echo -e "    ${RED}✗ FAILED - $status_code Server Error${NC}"
        FAILED_TESTS=$((FAILED_TESTS + 1))
        echo "$method|$endpoint|FAILED|$status_code|Server error" >> "$RESULTS_FILE"
    elif [ "$status_code" = "400" ]; then
        echo -e "    ${YELLOW}⚠ WARNING - 400 Bad Request${NC}"
        PASSED_TESTS=$((PASSED_TESTS + 1))
        echo "$method|$endpoint|WARNING|400|Bad request (may be expected)" >> "$RESULTS_FILE"
    elif [ "$status_code" = "401" ]; then
        echo -e "    ${YELLOW}⚠ WARNING - 401 Unauthorized${NC}"
        PASSED_TESTS=$((PASSED_TESTS + 1))
        echo "$method|$endpoint|WARNING|401|Unauthorized (expected)" >> "$RESULTS_FILE"
    elif [ "$status_code" = "403" ]; then
        echo -e "    ${YELLOW}⚠ WARNING - 403 Forbidden${NC}"
        PASSED_TESTS=$((PASSED_TESTS + 1))
        echo "$method|$endpoint|WARNING|403|Forbidden (expected)" >> "$RESULTS_FILE"
    else
        echo -e "    ${RED}✗ FAILED - Status: $status_code${NC}"
        FAILED_TESTS=$((FAILED_TESTS + 1))
        echo "$method|$endpoint|FAILED|$status_code|Unexpected status" >> "$RESULTS_FILE"
    fi
}

# Initialize results file
echo "Method|Endpoint|Status|StatusCode|Message" > "$RESULTS_FILE"

# Root and Health endpoints
test_endpoint "GET" "/" "Root endpoint - API info" "false" "200"
test_endpoint "GET" "/health" "Health check" "false" "200"
test_endpoint "GET" "/api/v1/health" "Detailed health check" "false" "200 503"

# Auth endpoints
echo -e "\n${YELLOW}========== AUTH ENDPOINTS ==========${NC}"
test_endpoint "GET" "/api/v1/auth/me" "Get current user profile" "true" "200 401"
test_endpoint "POST" "/api/v1/auth/logout" "Logout" "true" "200 401"

# User endpoints
echo -e "\n${YELLOW}========== USER ENDPOINTS ==========${NC}"
test_endpoint "GET" "/api/v1/users" "Get all users" "true" "200 401 403"
test_endpoint "GET" "/api/v1/users/00000000-0000-0000-0000-000000000000" "Get user by ID" "true" "200 401 403 404"
test_endpoint "GET" "/api/v1/users/students/by-id-number/TEST123" "Get student by ID number" "true" "200 401 403 404"
test_endpoint "GET" "/api/v1/users/students/rfid/TEST_RFID" "Get student by RFID" "false" "200 404"

# Item endpoints
echo -e "\n${YELLOW}========== ITEM ENDPOINTS ==========${NC}"
test_endpoint "GET" "/api/v1/items" "Get all items" "true" "200 401 403"
test_endpoint "GET" "/api/v1/items/00000000-0000-0000-0000-000000000000" "Get item by ID" "true" "200 401 403 404"
test_endpoint "GET" "/api/v1/items/by-serial/TEST123" "Get item by serial number" "true" "200 401 403 404"
test_endpoint "GET" "/api/v1/items/rfid/TEST_RFID" "Get item by RFID" "false" "200 404"

# Lent Items endpoints
echo -e "\n${YELLOW}========== LENT ITEMS ENDPOINTS ==========${NC}"
test_endpoint "GET" "/api/v1/lentItems" "Get all lent items" "true" "200 401 403"
test_endpoint "GET" "/api/v1/lentItems/00000000-0000-0000-0000-000000000000" "Get lent item by ID" "true" "200 401 403 404"

# Summary endpoint
echo -e "\n${YELLOW}========== SUMMARY ENDPOINTS ==========${NC}"
test_endpoint "GET" "/api/v1/summary" "Get overall summary" "true" "200 401 403"

# Archive endpoints
echo -e "\n${YELLOW}========== ARCHIVE ENDPOINTS ==========${NC}"
test_endpoint "GET" "/api/v1/archiveitems" "Get all archived items" "true" "200 401 403"
test_endpoint "GET" "/api/v1/archivelentitems" "Get all archived lent items" "true" "200 401"
test_endpoint "GET" "/api/v1/archiveusers" "Get all archived users" "true" "200 401 403"

# Print summary
echo -e "\n${MAGENTA}========================================${NC}"
echo -e "${MAGENTA}TEST SUMMARY${NC}"
echo -e "${MAGENTA}========================================${NC}"
echo "Total Tests:   $TOTAL_TESTS"
echo -e "${GREEN}Passed:        $PASSED_TESTS${NC}"
echo -e "${RED}Failed:        $FAILED_TESTS${NC}"
echo -e "${YELLOW}Skipped:       $SKIPPED_TESTS${NC}"
echo -e "${MAGENTA}========================================${NC}\n"

echo -e "${CYAN}Results saved to: $RESULTS_FILE${NC}\n"

# Exit with appropriate code
if [ $FAILED_TESTS -gt 0 ]; then
    exit 1
else
    exit 0
fi
