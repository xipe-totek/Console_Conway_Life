/*using ConsoleConwayLife.Models;
using ConsoleConwayLife.Services.Abstractions;
using ConsoleConwayLife.Services.Implementations;

namespace ConsoleConwayLifeTests;

public class LifeLogicTests
{
    private ILifeLogic _lifeLogic;
    
    [SetUp]
    public void Setup()
    {
        _lifeLogic = new LifeLogic();
    }

    /// <summary>
    /// Tests if exported cells are equal to imported ones
    /// </summary>
    [Test]
    public void TestCellsImportExport()
    {
        // Arrange
        var cells = new List<Cell>()
        {
            new Cell(10, 10),
            new Cell(20, 15),
            new Cell(30, 20)
        };

        // Act
        _lifeLogic.ImportCells(cells);

        var exportedCells = _lifeLogic.ExportCells();

        // Assert
        Assert.That(exportedCells, Is.EqualTo(cells));
    }

    [Test]
    [TestCase(10, 10, true)]
    [TestCase(20, 15, true)]
    [TestCase(30, 20, true)]
    [TestCase(10, 100, false)]
    public void TestIsThereACell(int x, int y, bool isCellHere)
    {
        // Arrange
        var cells = new List<Cell>()
        {
            new Cell(10, 10),
            new Cell(20, 15),
            new Cell(30, 20)
        };
        
        _lifeLogic.ImportCells(cells);
        
        // Act & Assert
        Assert.That(_lifeLogic.IsThereACell(x, y), Is.EqualTo(isCellHere));
    }

    [Test]
    public void TestGetNeighboursCount()
    {
        // Arrange
        var cells = new List<Cell>()
        {
            new Cell(10, 10),
            new Cell(10, 11),
            new Cell(9, 9),
            new Cell(10, 9),
            new Cell(11, 11),
            new Cell(10, 100)
        };
        
        _lifeLogic.ImportCells(cells);

        // Act
        var neighboursCount = _lifeLogic.GetNeighboursCount(10, 10);
        
        // Assert
        Assert.That(neighboursCount, Is.EqualTo(4));
    }
}*/