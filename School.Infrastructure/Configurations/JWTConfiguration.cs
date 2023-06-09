﻿using System;
namespace School.Infrastructure.Configurations
{
	public class JWTConfiguration
	{
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}
