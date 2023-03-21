using AutoMapper;
using ContentPlatformInterview.Models;

namespace ContentPlatformInterview.Helpers

{
    public class AutoMapingProfiles:Profile
    {
        public AutoMapingProfiles()
        {
            CreateMap<Product, ProductResponse>();
        }
    }
}
