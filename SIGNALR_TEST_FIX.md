# Fix for LentItemsServiceTests.cs

The `LentItemsService` now requires an `INotificationService` parameter. Update the test file as follows:

## Location
`BackendTechnicalAssetsManagementTest/Services/LentItemsServiceTests.cs`

## Changes Required

### 1. Add Mock Field Declaration (around line 20-30)
Add this line with the other mock declarations:
```csharp
private readonly Mock<INotificationService> _mockNotificationService;
```

### 2. Initialize Mock in Constructor (around line 40-50)
Add this line in the constructor where other mocks are initialized:
```csharp
_mockNotificationService = new Mock<INotificationService>();
```

### 3. Add Mock to Service Initialization (around line 60-70)
Update the `new LentItemsService(...)` call to include the notification service:

**Before:**
```csharp
_lentItemsService = new LentItemsService(
    _mockLentItemsRepository.Object,
    _mockMapper.Object,
    _mockUserRepository.Object,
    _mockItemRepository.Object,
    _mockArchiveLentItemsService.Object,
    _mockUserService.Object,
    _mockBarcodeGenerator.Object
);
```

**After:**
```csharp
_lentItemsService = new LentItemsService(
    _mockLentItemsRepository.Object,
    _mockMapper.Object,
    _mockUserRepository.Object,
    _mockItemRepository.Object,
    _mockArchiveLentItemsService.Object,
    _mockUserService.Object,
    _mockBarcodeGenerator.Object,
    _mockNotificationService.Object  // <-- ADD THIS LINE
);
```

### 4. Add Using Statement (at the top of the file)
Make sure this using statement is present:
```csharp
using BackendTechnicalAssetsManagement.src.IService;
```

## Complete Example

Here's what the relevant sections should look like after the changes:

```csharp
using BackendTechnicalAssetsManagement.src.IService;
// ... other using statements ...

public class LentItemsServiceTests
{
    private readonly Mock<ILentItemsRepository> _mockLentItemsRepository;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IItemRepository> _mockItemRepository;
    private readonly Mock<IArchiveLentItemsService> _mockArchiveLentItemsService;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IBarcodeGeneratorService> _mockBarcodeGenerator;
    private readonly Mock<INotificationService> _mockNotificationService;  // <-- ADD THIS
    private readonly LentItemsService _lentItemsService;

    public LentItemsServiceTests()
    {
        _mockLentItemsRepository = new Mock<ILentItemsRepository>();
        _mockUserRepository = new Mock<IUserRepository>();
        _mockItemRepository = new Mock<IItemRepository>();
        _mockArchiveLentItemsService = new Mock<IArchiveLentItemsService>();
        _mockUserService = new Mock<IUserService>();
        _mockMapper = new Mock<IMapper>();
        _mockBarcodeGenerator = new Mock<IBarcodeGeneratorService>();
        _mockNotificationService = new Mock<INotificationService>();  // <-- ADD THIS

        // ... barcode generator setup ...

        _lentItemsService = new LentItemsService(
            _mockLentItemsRepository.Object,
            _mockMapper.Object,
            _mockUserRepository.Object,
            _mockItemRepository.Object,
            _mockArchiveLentItemsService.Object,
            _mockUserService.Object,
            _mockBarcodeGenerator.Object,
            _mockNotificationService.Object  // <-- ADD THIS
        );
    }
    
    // ... rest of the tests ...
}
```

## Optional: Add Notification Tests

You can also add tests to verify that notifications are sent correctly:

```csharp
[Fact]
public async Task UpdateAsync_WhenStatusChangesFromPendingToApproved_ShouldSendNotification()
{
    // Arrange
    var lentItemId = Guid.NewGuid();
    var userId = Guid.NewGuid();
    var itemId = Guid.NewGuid();
    
    var lentItem = new LentItems
    {
        Id = lentItemId,
        ItemId = itemId,
        UserId = userId,
        Status = "Pending",
        ItemName = "Test Laptop",
        BorrowerFullName = "John Doe"
    };
    
    var item = new Item
    {
        Id = itemId,
        ItemName = "Test Laptop",
        Status = ItemStatus.Reserved,
        Condition = ItemCondition.Good
    };
    
    var updateDto = new UpdateLentItemDto { Status = "Approved" };
    
    _mockLentItemsRepository
        .Setup(x => x.GetByIdAsync(lentItemId))
        .ReturnsAsync(lentItem);
    
    _mockItemRepository
        .Setup(x => x.GetByIdAsync(itemId))
        .ReturnsAsync(item);
    
    _mockItemRepository
        .Setup(x => x.UpdateAsync(It.IsAny<Item>()))
        .Returns(Task.CompletedTask);
    
    _mockLentItemsRepository
        .Setup(x => x.UpdateAsync(It.IsAny<LentItems>()))
        .Returns(Task.CompletedTask);
    
    _mockLentItemsRepository
        .Setup(x => x.SaveChangesAsync())
        .ReturnsAsync(true);
    
    _mockNotificationService
        .Setup(x => x.SendApprovalNotificationAsync(
            It.IsAny<Guid>(),
            It.IsAny<Guid?>(),
            It.IsAny<string>(),
            It.IsAny<string>()))
        .Returns(Task.CompletedTask);
    
    // Act
    var result = await _lentItemsService.UpdateAsync(lentItemId, updateDto);
    
    // Assert
    Assert.True(result);
    _mockNotificationService.Verify(
        x => x.SendApprovalNotificationAsync(
            lentItemId,
            userId,
            "Test Laptop",
            "John Doe"),
        Times.Once);
}
```
