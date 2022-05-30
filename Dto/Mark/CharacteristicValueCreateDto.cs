namespace TinkoffWatcher_Api.Dto.Mark
{
    public class CharacteristicValueCreateDto
    {
        public string Description { get; set; }
        public int? IntValue { get; set; }
        public bool? BoolValue { get; set; }
    }
}