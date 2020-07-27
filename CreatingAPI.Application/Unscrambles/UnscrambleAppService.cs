﻿using AutoMapper;
using CreatingAPI.Application.Core;
using CreatingAPI.Application.Unscrambles.Interfaces;
using CreatingAPI.Application.Unscrambles.ViewModels;
using CreatingAPI.Domain.Unscrambles;
using CreatingAPI.Domain.Unscrambles.Interfaces;
using CreatingAPI.Domain.Unscrambles.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Unscrambles
{
    public class UnscrambleAppService : IUnscrambleAppService
    {
        private readonly IUnscrambleService _unscrambleService;
        private readonly IMapper _mapper;

        public UnscrambleAppService(IUnscrambleService unscrambleService, IMapper mapper)
        {
            _unscrambleService = unscrambleService;
            _mapper = mapper;
        }

        public async Task<ResultResponse> CreateUnscramble(UnscrambleCreationViewModel unscrambleCreationViewModel)
        {
            var unscramble = _mapper.Map<Unscramble>(unscrambleCreationViewModel);
            var exercises = _mapper.Map<IEnumerable<Exercise>>(unscrambleCreationViewModel.Exercises);

            var businessResult = await _unscrambleService.CreateUnscramble(unscramble, exercises);

            return new ResultResponse(businessResult, Operation.CREATE);
        }

        public async Task<ResultResponse> UpdateUnscramble(int id, UnscrambleCreationViewModel unscrambleCreationViewModel)
        {
            var unscramble = _mapper.Map<Unscramble>(unscrambleCreationViewModel);
            var exercises = _mapper.Map<IEnumerable<Exercise>>(unscrambleCreationViewModel.Exercises);

            var businessResult = await _unscrambleService.UpdateUnscramble(id, unscramble, exercises);

            return new ResultResponse(businessResult, Operation.UPDATE);
        }

        public async Task<ResultResponse> DeleteUnscramble(int idUnscramble)
        {
            var businessResult = await _unscrambleService.DeleteUnscramble(idUnscramble);

            return new ResultResponse(businessResult, Operation.DELETE);
        }

        public async Task<UnscrambleViewModel> GetUnscramble(int idUnscramble)
        {
            var unscramble = await _unscrambleService.GetUnscramble(idUnscramble);

            var unscrambleViewModel = _mapper.Map<UnscrambleViewModel>(unscramble);

            return unscrambleViewModel;
        }

        public async Task<IEnumerable<ShuffledExercise>> GetShuffledExercises(int idUnscramble, bool randomizeOrder)
        {
            return await _unscrambleService.GetShuffledExercises(idUnscramble, randomizeOrder);
        }
    }
}
