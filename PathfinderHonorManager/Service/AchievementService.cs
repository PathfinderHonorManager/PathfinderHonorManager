﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PathfinderHonorManager.DataAccess;
using PathfinderHonorManager.Dto.Outgoing;
using PathfinderHonorManager.Model;
using PathfinderHonorManager.Service.Interfaces;

namespace PathfinderHonorManager.Service
{
    public class AchievementService : IAchievementService
    {
        private readonly PathfinderContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<AchievementService> _logger;

        public AchievementService(PathfinderContext dbContext, IMapper mapper, ILogger<AchievementService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<AchievementDto>> GetAllAsync(CancellationToken token)
        {
            _logger.LogInformation("Getting all achievements");
            var achievements = await _dbContext.Achievements
                .Include(a => a.PathfinderClass)
                .Include(a => a.Category)
                .OrderBy(a => a.Grade).ThenBy(a => a.Level)
                .ToListAsync(token);

            return _mapper.Map<ICollection<AchievementDto>>(achievements);
        }

        public async Task<AchievementDto> GetByIdAsync(Guid id, CancellationToken token)
        {
            _logger.LogInformation($"Getting achievement by ID: {id}");
            var achievement = await _dbContext.Achievements
                .Include(a => a.PathfinderClass)
                .Include(a => a.Category)
                .SingleOrDefaultAsync(a => a.AchievementID == id, token);

            return achievement == null ? null : _mapper.Map<AchievementDto>(achievement);
        }
    }
}
