# Generate HTML Report from API Test Results
# Usage: .\generate-test-report.ps1 -CsvFile "api-test-results_20250101_120000.csv"

param(
    [Parameter(Mandatory=$false)]
    [string]$CsvFile = ""
)

# Find the most recent CSV file if not specified
if ([string]::IsNullOrEmpty($CsvFile)) {
    $CsvFile = Get-ChildItem -Filter "api-test-results_*.csv" | 
        Sort-Object LastWriteTime -Descending | 
        Select-Object -First 1 -ExpandProperty Name
    
    if ([string]::IsNullOrEmpty($CsvFile)) {
        Write-Host "No test results found. Please run test-api-endpoints.ps1 first." -ForegroundColor Red
        exit 1
    }
    
    Write-Host "Using most recent results: $CsvFile" -ForegroundColor Cyan
}

# Read CSV
$results = Import-Csv $CsvFile

# Calculate statistics
$total = $results.Count
$passed = ($results | Where-Object { $_.Status -eq "PASSED" }).Count
$failed = ($results | Where-Object { $_.Status -eq "FAILED" }).Count
$warning = ($results | Where-Object { $_.Status -eq "WARNING" }).Count
$skipped = ($results | Where-Object { $_.Status -eq "SKIPPED" }).Count

$passRate = if ($total -gt 0) { [math]::Round(($passed / $total) * 100, 2) } else { 0 }

# Generate HTML
$html = @"
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>API Test Report</title>
    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            padding: 20px;
            min-height: 100vh;
        }
        .container {
            max-width: 1200px;
            margin: 0 auto;
            background: white;
            border-radius: 10px;
            box-shadow: 0 10px 40px rgba(0,0,0,0.2);
            overflow: hidden;
        }
        .header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 30px;
            text-align: center;
        }
        .header h1 { font-size: 2.5em; margin-bottom: 10px; }
        .header p { font-size: 1.1em; opacity: 0.9; }
        .stats {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 20px;
            padding: 30px;
            background: #f8f9fa;
        }
        .stat-card {
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            text-align: center;
        }
        .stat-card h3 { font-size: 2em; margin-bottom: 5px; }
        .stat-card p { color: #666; font-size: 0.9em; text-transform: uppercase; }
        .stat-card.total { border-left: 4px solid #667eea; }
        .stat-card.passed { border-left: 4px solid #28a745; }
        .stat-card.failed { border-left: 4px solid #dc3545; }
        .stat-card.warning { border-left: 4px solid #ffc107; }
        .stat-card.skipped { border-left: 4px solid #6c757d; }
        .results {
            padding: 30px;
        }
        .results h2 {
            margin-bottom: 20px;
            color: #333;
            border-bottom: 2px solid #667eea;
            padding-bottom: 10px;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 30px;
        }
        th, td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        th {
            background: #667eea;
            color: white;
            font-weight: 600;
            position: sticky;
            top: 0;
        }
        tr:hover { background: #f8f9fa; }
        .status {
            padding: 5px 10px;
            border-radius: 4px;
            font-weight: 600;
            font-size: 0.85em;
            display: inline-block;
        }
        .status.PASSED { background: #d4edda; color: #155724; }
        .status.FAILED { background: #f8d7da; color: #721c24; }
        .status.WARNING { background: #fff3cd; color: #856404; }
        .status.SKIPPED { background: #e2e3e5; color: #383d41; }
        .method {
            padding: 4px 8px;
            border-radius: 4px;
            font-weight: 600;
            font-size: 0.8em;
            display: inline-block;
        }
        .method.GET { background: #d1ecf1; color: #0c5460; }
        .method.POST { background: #d4edda; color: #155724; }
        .method.PATCH { background: #fff3cd; color: #856404; }
        .method.DELETE { background: #f8d7da; color: #721c24; }
        .footer {
            background: #f8f9fa;
            padding: 20px;
            text-align: center;
            color: #666;
            font-size: 0.9em;
        }
        .progress-bar {
            width: 100%;
            height: 30px;
            background: #e9ecef;
            border-radius: 15px;
            overflow: hidden;
            margin: 20px 0;
        }
        .progress-fill {
            height: 100%;
            background: linear-gradient(90deg, #28a745 0%, #20c997 100%);
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            font-weight: 600;
            transition: width 0.3s ease;
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="header">
            <h1>🚀 API Test Report</h1>
            <p>Generated on $(Get-Date -Format "MMMM dd, yyyy HH:mm:ss")</p>
        </div>
        
        <div class="stats">
            <div class="stat-card total">
                <h3>$total</h3>
                <p>Total Tests</p>
            </div>
            <div class="stat-card passed">
                <h3>$passed</h3>
                <p>Passed</p>
            </div>
            <div class="stat-card failed">
                <h3>$failed</h3>
                <p>Failed</p>
            </div>
            <div class="stat-card warning">
                <h3>$warning</h3>
                <p>Warnings</p>
            </div>
            <div class="stat-card skipped">
                <h3>$skipped</h3>
                <p>Skipped</p>
            </div>
        </div>
        
        <div class="results">
            <h2>📊 Pass Rate</h2>
            <div class="progress-bar">
                <div class="progress-fill" style="width: ${passRate}%">
                    ${passRate}%
                </div>
            </div>
            
            <h2>📋 Test Results</h2>
            <table>
                <thead>
                    <tr>
                        <th>Method</th>
                        <th>Endpoint</th>
                        <th>Status</th>
                        <th>Status Code</th>
                        <th>Message</th>
                    </tr>
                </thead>
                <tbody>
"@

# Add rows
foreach ($result in $results) {
    $html += @"
                    <tr>
                        <td><span class="method $($result.Method)">$($result.Method)</span></td>
                        <td><code>$($result.Endpoint)</code></td>
                        <td><span class="status $($result.Status)">$($result.Status)</span></td>
                        <td>$($result.StatusCode)</td>
                        <td>$($result.Message)</td>
                    </tr>
"@
}

$html += @"
                </tbody>
            </table>
        </div>
        
        <div class="footer">
            <p>Technical Assets Management System - API Testing</p>
            <p>Source: $CsvFile</p>
        </div>
    </div>
</body>
</html>
"@

# Save HTML
$htmlFile = $CsvFile -replace '\.csv$', '.html'
$html | Out-File -FilePath $htmlFile -Encoding UTF8

Write-Host "`n✓ HTML report generated: $htmlFile" -ForegroundColor Green
Write-Host "Opening in browser..." -ForegroundColor Cyan

# Open in default browser
Start-Process $htmlFile
