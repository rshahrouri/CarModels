namespace CarModels.DTOs;

// Property names in this class violates naming conventions to maintain consistency with the JSON properties returned by external APIs

public class Model
{
    public int Make_ID { get; set; }

    public string Make_Name { get; set; }

    public int Model_ID { get; set; }

    public string Model_Name { get; set; }
}
