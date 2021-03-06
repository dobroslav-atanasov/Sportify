﻿namespace Sportify.Services
{
    using Data;
    using Data.Models;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Identity;

    public abstract class BaseService
    {
        protected BaseService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.Context = context;
            this.Mapper = mapper;
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public SportifyDbContext Context { get; set; }

        public IMapper Mapper { get; set; }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> SignInManager { get; set; }
    }
}