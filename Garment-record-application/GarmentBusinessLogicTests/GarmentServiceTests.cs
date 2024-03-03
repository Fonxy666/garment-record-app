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
                Id = (uint)i + 1,
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
        var searchedGarment = _garmentService!.SearchGarment(1);
        Assert.That(searchedGarment != null);
    }
    
    [Test, Order(4)]
    public void Sort_Garment_By_Id()
    {
        _garmentService!.SortGarments("id");
        Assert.That(_garmentService!.GarmentList![0].Id == 1);
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
        for (var i = 0; i < 10; i++)
        {
            _garmentService!.DeleteGarment((uint)i+1);
        }
        
        Assert.That(_garmentService!.GarmentList!.Count == 0);
    }
}