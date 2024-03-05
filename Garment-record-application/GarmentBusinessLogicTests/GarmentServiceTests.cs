using GarmentBusinessLogic.Service;
using GarmentBusinessLogic.Service.Logger;
using GarmentRecordLibrary.Model;
using GarmentRecordLibrary.Model.Enum;

namespace GarmentBusinessLogicTests;

public class Tests
{
    private IGarmentService? _garmentService;
    
    [SetUp]
    public void Setup()
    {
        var testPath = "GarmentDataTests.json";
        ILogger logger = new Logger();
        _garmentService = new GarmentService(testPath, logger);
    }

    [Test, Order(1)]
    public void Add_Garment_Return_Okay_With_Valid_Parameters()
    {
        for (var i = 9; i >= 0; i--)
        {
            _garmentService!.AddGarment(new Garment
            {
                Id = (uint)i,
                BrandName = i % 5 == 0? "Test brand name" : "Other test brand name",
                Color = i % 2 == 0? "White" : "Grey",
                Purchase = new DateTime(2024, 2, i + 1),
                Size = i % 3 == 0? GarmentSize.S : GarmentSize.L
            });
        }
        
        Assert.That(_garmentService!.GarmentList!.Count, Is.EqualTo(10));
    }
    
    [Test, Order(2)]
    public void Update_Garment_Return_Okay_With_Valid_Parameters()
    {
        var updatedGarment = new Garment
        {
            Id = _garmentService!.GarmentList![0].Id,
            BrandName = "Updated test brand name",
            Color = "White",
            Purchase = DateTime.Now,
            Size = GarmentSize.L
        };
        _garmentService!.UpdateGarment(_garmentService!.GarmentList![0].Id, updatedGarment);
        
        Assert.That(_garmentService.GarmentList![0].BrandName, Is.EqualTo(updatedGarment.BrandName));
    }
    
    [Test, Order(3)]
    public void Search_Garment_Return_Okay_With_Valid_Parameters()
    {
        var searchedGarment = _garmentService!.SearchGarment(_garmentService!.GarmentList![0].Id);
        Assert.That(searchedGarment != null);
    }
    
    [Test, Order(4)]
    public void Sort_Garment_By_Id()
    {
        _garmentService!.SortGarments("id");
        Assert.That(_garmentService!.GarmentList![0].Id == _garmentService!.GarmentList.Min(garment => garment.Id));
    }
    
    [Test, Order(5)]
    public void Sort_Garment_By_BrandName()
    {
        _garmentService!.SortGarments("name");
        Assert.That(_garmentService!.GarmentList![0].BrandName == "Other test brand name");
        Assert.That(_garmentService!.GarmentList![_garmentService!.GarmentList!.Count-2].BrandName == "Test brand name");
    }
    
    [Test, Order(6)]
    public void Sort_Garment_By_Color()
    {
        _garmentService!.SortGarments("color");
        Assert.That(_garmentService!.GarmentList![0].Color == "Grey");
        Assert.That(_garmentService!.GarmentList![5].Color == "White");
    }
    
    [Test, Order(7)]
    public void Sort_Garment_By_Purchase()
    {
        _garmentService!.SortGarments("purchase");
        Assert.That(_garmentService!.GarmentList![0].Purchase == new DateTime(2024,2,1));
    }
    
    [Test, Order(8)]
    public void Sort_Garment_By_Size()
    {
        _garmentService!.SortGarments("size");
        Assert.That(_garmentService!.GarmentList![0].Size == GarmentSize.S);
        Assert.That(_garmentService!.GarmentList![_garmentService!.GarmentList!.Count-2].Size == GarmentSize.L);
    }
    
    [Test, Order(9)]
    public void Delete_Garment_Return_Okay_With_Valid_Parameters()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            if (_garmentService!.GarmentList == null || !_garmentService!.GarmentList.Any())
            {
                throw new InvalidOperationException("GarmentList is already empty. No deletion needed.");
            }
            for (var i = _garmentService!.GarmentList!.Min(garment => garment.Id); i <= _garmentService!.GarmentList!.Max(garment => garment.Id); i++)
            {
                _garmentService!.DeleteGarment(i);
            }
        });
        Assert.That(_garmentService!.GarmentList, Is.Empty, "GarmentList should be empty after deleting all garments");
    }
}