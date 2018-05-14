using AutoMapper;
using MovieRental.Dtos;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRental.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Customer and Customer DTO should  be mappable to each other
            
            // Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();

            // Dto to Domain
            Mapper.CreateMap<CustomerDto, Customer>();
            Mapper.CreateMap<MovieDto, Movie>();

            // Domain to Dto
            Mapper.CreateMap<MembershipType,MembershipTypeDto> ();
            Mapper.CreateMap<Genre, GenreDto>();

            // Dto to Domain - Update fails as DTO to Customer, updates ID field too
            // While Mapping objects, for member ID, set an option to be implemented.
            // Option defined here for ID is to just ignore.

            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            //Mappings for Movie and Movie DTO
            Mapper.CreateMap<MovieDto, Movie>()
                .ForMember(movie => movie.Id, option => option.Ignore());
        }
    }
}