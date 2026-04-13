using System.ComponentModel;

namespace Sampaio.Shared.Enums
{
    public enum EPropertyFuelType
    {
        [Description("Gasolina")]Gasoline,
        [Description("Alcool")]Alcohol,
        [Description("Flex")]Flex,
        [Description("Gasolina e GNV")]GasolineAndCng,
        [Description("Alcool e GNV")]AlcoholAndCng,
        [Description("Flex e GNV")]FlexAndCng
    }
}