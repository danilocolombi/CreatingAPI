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

        public async Task<ResultResponse> CreateTicTacToe(TicTacToeCreationViewModel ticTacToeCreationViewModel)
        {
            var ticTacToe = _mapper.Map<TicTacToe>(ticTacToeCreationViewModel);
            var squares = _mapper.Map<IEnumerable<TicTacToeSquare>>(ticTacToeCreationViewModel.Squares);

            var businessResult = await _ticTacToeService.CreateTicTacToe(ticTacToe, squares);

            return new ResultResponse(businessResult, Operation.CREATE);
        }

        public async Task<ResultResponse> UpdateTicTacToe(int id, TicTacToeCreationViewModel ticTacToeCreationViewModel)
        {
            var ticTacToe = _mapper.Map<TicTacToe>(ticTacToeCreationViewModel);
            var squares = _mapper.Map<IEnumerable<TicTacToeSquare>>(ticTacToeCreationViewModel.Squares);

            var businessResult = await _ticTacToeService.UpdateTicTacToe(id, ticTacToe, squares);

            return new ResultResponse(businessResult, Operation.UPDATE);
        }

        public async Task<ResultResponse> DeleteTicTacToe(int id)
        {
            var businessResult = await _ticTacToeService.DeleteTicTacToe(id);

            return new ResultResponse(businessResult, Operation.DELETE);
        }

        public async Task<TicTacToeViewModel> GetTicTacToe(int id)
        {
            var ticTacToe = await _ticTacToeService.GetTicTacToe(id);

            var ticTacToeViewModel = _mapper.Map<TicTacToeViewModel>(ticTacToe);

            return ticTacToeViewModel;
        }
    }
}
