using AutoMapper;
using CreatingAPI.Application.Core;
using CreatingAPI.Application.TicTacToes.Interfaces;
using CreatingAPI.Application.TicTacToes.ViewModels;
using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.TicTacToes.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Application.TicTacToes
{
    public class TicTacToeAppService : ITicTacToeAppService
    {
        private readonly ITicTacToeService _ticTacToeService;
        private readonly IMapper _mapper;

        public TicTacToeAppService(ITicTacToeService ticTacToeService, IMapper mapper)
        {
            _ticTacToeService = ticTacToeService;
            _mapper = mapper;
        }

        public async Task<ResultResponse> CreateAsync(TicTacToeCreationViewModel ticTacToeCreationViewModel)
        {
            var ticTacToe = _mapper.Map<TicTacToe>(ticTacToeCreationViewModel);
            var squares = _mapper.Map<IEnumerable<TicTacToeSquare>>(ticTacToeCreationViewModel.Squares);

            var businessResult = await _ticTacToeService.CreateAsync(ticTacToe, squares);

            return new ResultResponse(businessResult, Operation.CREATE);
        }

        public async Task<ResultResponse> UpdateAsync(int id, TicTacToeCreationViewModel ticTacToeCreationViewModel)
        {
            var ticTacToe = _mapper.Map<TicTacToe>(ticTacToeCreationViewModel);
            var squares = _mapper.Map<IEnumerable<TicTacToeSquare>>(ticTacToeCreationViewModel.Squares);

            var businessResult = await _ticTacToeService.UpdateAsync(id, ticTacToe, squares);

            return new ResultResponse(businessResult, Operation.UPDATE);
        }

        public async Task<ResultResponse> DeleteAsync(int id)
        {
            var businessResult = await _ticTacToeService.DeleteAsync(id);

            return new ResultResponse(businessResult, Operation.DELETE);
        }

        public async Task<TicTacToeViewModel> GetAsync(int id)
        {
            var ticTacToe = await _ticTacToeService.GetAsync(id);

            var ticTacToeViewModel = _mapper.Map<TicTacToeViewModel>(ticTacToe);

            return ticTacToeViewModel;
        }
    }
}
