using GarmentBusinessLogic.Service;
using GarmentBusinessLogic.Service.Logger;
using GarmentBusinessLogic.Ui;

ILogger logger = new Logger();
IGarmentService garmentService = new GarmentService("GarmentData.json", logger);
var ui = new GarmentConsoleUi(logger, garmentService);

ui.Run();