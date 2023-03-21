using System.Collections.Generic;

namespace ContentPlatformInterview.Utils
{
    public class TypeProductTax
    {
        public static double ProductsTax(string producType) => producType switch
        {
            "Drink" => 0.05,
            "Food" => 0.06,
            "Clothing" => 0.07,
            _ => throw new NotImplementedException(),
        };  
    }
}
