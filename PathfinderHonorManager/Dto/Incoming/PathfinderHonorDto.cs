﻿using System;

namespace PathfinderHonorManager.Dto.Incoming
{
    public class PathfinderHonorDto
    {
        public Guid HonorID { get; set; }
        public Guid PathfinderID { get; set; }
        public string StatusCode { get; set; }
    }
}
