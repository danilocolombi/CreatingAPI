using AutoMapper;
using CreatingAPI.Application.Core;
using CreatingAPI.Application.Unscrumbles.Interfaces;
using CreatingAPI.Application.Unscrumbles.ViewModels;
using CreatingAPI.Domain.Unscrumbles;
using CreatingAPI.Domain.Unscrumbles.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Unscrumbles
{
    public class UnscrumbleAppService: IUnscrumbleAppService
    {
        private readonly IUnscrumbleService _unscrumbleService;
        private readonly IMapper _mapper;

        public UnscrumbleAppService(IUnscrumbleService unscrumbleService, IMapper mapper)
        {
            _unscrumbleService = unscrumbleService;
            _mapper = mapper;
        }

        public async Task<ResultResponse> CreateUnscrumble(UnscrumbleCreationViewModel unscrumbleCreationViewModel)
        {
            var unscrumble = _mapper.Map<Unscrumble>(unscrumbleCreationViewModel);
            var exercises = _mapper.Map<IEnumerable<Exercise>>(unscrumbleCreationViewModel.Exercises);

            var businessResult = await _unscrumbleService.CreateUnscrumble(unscrumble, exercises);

            return new ResultResponse(businessResult, Operation.CREATE);
        }

        public async Task<ResultResponse> UpdateUnscrumble(int id, UnscrumbleCreationViewModel unscrumbleCreationViewModel)
        {
            var unscrumble = _mapper.Map<Unscrumble>(unscrumbleCreationViewModel);
            var exercises = _mapper.Map<IEnumerable<Exercise>>(unscrumbleCreationViewModel.Exercises);

            var businessResult = await _unscrumbleService.UpdateUnscrumble(id, unscrumble, exercises);

            return new ResultResponse(businessResult, Operation.UPDATE);
        }

        public async Task<ResultResponse> DeleteUnscrumble(int idUnscrumble)
        {
            var businessResult = await _unscrumbleService.DeleteUnscrumble(idUnscrumble);

            return new ResultResponse(businessResult, Operation.DELETE);
        }

        public async Task<UnscrumbleViewModel> GetUnscrumble(int idUnscrumble)
        {
            var unscrumble = await _unscrumbleService.GetUnscrumble(idUnscrumble);

            var unscrumbleViewModel = _mapper.Map<UnscrumbleViewModel>(unscrumble);

            return unscrumbleViewModel;
        }
    }
}
