# Barcode Service Consolidation Guide

## Overview

The barcode generation logic has been consolidated from two separate utility classes into a single, unified service. This improves maintainability, reduces code duplication, and follows dependency injection best practices.

## What Changed

### Before (Deprecated)
```csharp
// Two separate utility classes
using BackendTechnicalAssetsManagement.src.Utils;

// Generate barcode text
string barcodeText = BarcodeGenerator.GenerateItemBarcode(serialNumber);

// Generate barcode image
byte[]? barcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(barcodeText);
```

### After (Current)
```csharp
// Single unified service via dependency injection
using BackendTechnicalAssetsManagement.src.IService;

public class YourService
{
    private readonly IBarcodeGeneratorService _barcodeGenerator;
    
    public YourService(IBarcodeGeneratorService barcodeGenerator)
    {
        _barcodeGenerator = barcodeGenerator;
    }
    
    public async Task DoSomething()
    {
        // Generate barcode text
        string barcodeText = _barcodeGenerator.GenerateItemBarcode(serialNumber);
        
        // Generate barcode image
        byte[]? barcodeImage = _barcodeGenerator.GenerateBarcodeImage(barcodeText);
    }
}
```

## Benefits

1. **Single Responsibility**: All barcode-related logic is in one place
2. **Dependency Injection**: Easier to test and mock
3. **No Code Duplication**: One implementation to maintain
4. **Better Testability**: Can easily mock the service in unit tests
5. **Consistent API**: All barcode operations through one interface

## Migration Steps

### For Service Classes

1. **Add the dependency** to your constructor:
   ```csharp
   private readonly IBarcodeGeneratorService _barcodeGenerator;
   
   public YourService(IBarcodeGeneratorService barcodeGenerator)
   {
       _barcodeGenerator = barcodeGenerator;
   }
   ```

2. **Replace utility calls**:
   ```csharp
   // OLD:
   string barcode = BarcodeGenerator.GenerateItemBarcode(serialNumber);
   byte[]? image = BarcodeImageUtil.GenerateBarcodeImageBytes(barcode);
   
   // NEW:
   string barcode = _barcodeGenerator.GenerateItemBarcode(serialNumber);
   byte[]? image = _barcodeGenerator.GenerateBarcodeImage(barcode);
   ```

### For Tests

1. **Update the performance flag**:
   ```csharp
   // OLD:
   BarcodeImageUtil.SkipImageGeneration = true;
   
   // NEW:
   BarcodeGeneratorService.SkipImageGeneration = true;
   ```

2. **Mock the service**:
   ```csharp
   var mockBarcodeGenerator = new Mock<IBarcodeGeneratorService>();
   mockBarcodeGenerator
       .Setup(x => x.GenerateItemBarcode(It.IsAny<string>()))
       .Returns("ITEM-TEST-001");
   mockBarcodeGenerator
       .Setup(x => x.GenerateBarcodeImage(It.IsAny<string>()))
       .Returns((byte[]?)null);
   ```

## API Reference

### IBarcodeGeneratorService

```csharp
public interface IBarcodeGeneratorService
{
    /// <summary>
    /// Generates a barcode text for lent items using the format: LENT-YYYYMMDD-XXX
    /// </summary>
    Task<string> GenerateLentItemBarcodeAsync(DateTime? date = null);
    
    /// <summary>
    /// Generates a barcode text for items using the format: ITEM-{serialNumber}
    /// </summary>
    string GenerateItemBarcode(string serialNumber);
    
    /// <summary>
    /// Generates a Code 128 barcode image as a PNG byte array for the given text.
    /// Returns null if text is invalid or if SkipImageGeneration flag is set (for testing).
    /// </summary>
    byte[]? GenerateBarcodeImage(string barcodeText);
}
```

## Updated Services

The following services have been updated to use the new consolidated service:

- ✅ `LentItemsService` - Uses `IBarcodeGeneratorService` for lent item barcodes
- ✅ `ItemService` - Uses `IBarcodeGeneratorService` for item barcodes
- ✅ `LentItemsServiceTests` - Updated to use new flag location

## Removed Classes

The following utility classes have been **completely removed** and replaced with the unified service:

- ✅ `BarcodeGenerator` (Utils) - **DELETED** - Use `BarcodeGeneratorService.GenerateItemBarcodeStatic()` for static contexts
- ✅ `BarcodeImageUtil` (Utils) - **DELETED** - Use `BarcodeGeneratorService.GenerateBarcodeImage()` or `GenerateBarcodeImageStatic()`

**Note**: The seed data in `ModelBuilderExtensions` now uses the static methods from `BarcodeGeneratorService`.

## Performance Optimization

The service includes a static flag for test performance:

```csharp
BarcodeGeneratorService.SkipImageGeneration = true;
```

When set to `true`, barcode image generation is skipped (returns `null`). This is useful in tests where:
- Image generation takes 400-700ms per barcode
- Tests only need barcode text, not actual images
- Faster test execution is desired

## Testing Example

```csharp
public class YourServiceTests
{
    private readonly Mock<IBarcodeGeneratorService> _mockBarcodeGenerator;
    private readonly YourService _service;
    
    public YourServiceTests()
    {
        _mockBarcodeGenerator = new Mock<IBarcodeGeneratorService>();
        
        // Setup default behavior
        _mockBarcodeGenerator
            .Setup(x => x.GenerateItemBarcode(It.IsAny<string>()))
            .Returns((string sn) => $"ITEM-{sn}");
            
        _mockBarcodeGenerator
            .Setup(x => x.GenerateBarcodeImage(It.IsAny<string>()))
            .Returns((byte[]?)null); // Skip image generation in tests
            
        _service = new YourService(_mockBarcodeGenerator.Object);
    }
    
    [Fact]
    public async Task TestMethod()
    {
        // Your test code here
        // The barcode service is fully mocked
    }
}
```

## Questions?

If you have questions about this consolidation or need help migrating your code, please refer to:
- `BarcodeGeneratorService.cs` - The unified implementation
- `IBarcodeGeneratorService.cs` - The service interface
- `LentItemsService.cs` or `ItemService.cs` - Example usage

## Static Methods for Seed Data

For static contexts like seed data where dependency injection is not available, use the static helper methods:

```csharp
// In ModelBuilderExtensions.Seed() or other static contexts
string barcodeText = BarcodeGeneratorService.GenerateItemBarcodeStatic(serialNumber);
byte[]? barcodeImage = BarcodeGeneratorService.GenerateBarcodeImageStatic(barcodeText);
```

These static methods are wrappers around the same core logic used by the instance methods.

---

**Last Updated**: November 25, 2025  
**Status**: ✅ Complete - All utilities removed, unified service in place
