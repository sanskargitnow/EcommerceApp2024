namespace ApiWeb.DTOs
{
    public class CreateCategoryDtos
    {

       public int Id { get; set; }
        public string Name { get; set; }  // The name of the category
        public int DisplayOrder { get; set; }
    }
}
