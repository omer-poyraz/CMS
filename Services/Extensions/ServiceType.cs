namespace Services.Extensions
{
    public class ServiceType
    {
        public string Service { get; set; }

        public ServiceType(int type)
        {
            if (type == 1)
                Service = "Authentication";
            else if (type == 2)
                Service = "Blog";
            else if (type == 3)
                Service = "Brand";
            else if (type == 4)
                Service = "Category";
            else if (type == 5)
                Service = "Color";
            else if (type == 6)
                Service = "ColorGroup";
            else if (type == 7)
                Service = "Comment";
            else if (type == 8)
                Service = "Contact";
            else if (type == 9)
                Service = "File";
            else if (type == 10)
                Service = "Folder";
            else if (type == 11)
                Service = "Footer";
            else if (type == 12)
                Service = "Header";
            else if (type == 13)
                Service = "Image";
            else if (type == 14)
                Service = "Language";
            else if (type == 15)
                Service = "Media";
            else if (type == 16)
                Service = "Page";
            else if (type == 17)
                Service = "ParentSidebar";
            else if (type == 18)
                Service = "Popup";
            else if (type == 19)
                Service = "Product";
            else if (type == 20)
                Service = "Seo";
            else if (type == 21)
                Service = "Sidebar";
            else if (type == 22)
                Service = "Size";
            else if (type == 23)
                Service = "SizeGroup";
            else if (type == 24)
                Service = "Slider";
            else if (type == 25)
                Service = "SliderGroup";
            else if (type == 26)
                Service = "SocialMedia";
            else if (type == 27)
                Service = "Unit";
            else if (type == 28)
                Service = "UnPublicFile";
            else if (type == 29)
                Service = "User";
            else if (type == 30)
                Service = "Created";
            else if (type == 31)
                Service = "Updated";
            else if (type == 32)
                Service = "Deleted";
        }
    }
}
