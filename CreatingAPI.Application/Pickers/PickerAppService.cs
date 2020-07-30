using AutoMapper;
using CreatingAPI.Application.Core;
using CreatingAPI.Application.Pickers.Interfaces;
using CreatingAPI.Application.Pickers.ViewModels;
using CreatingAPI.Domain.Pickers;
using CreatingAPI.Domain.Pickers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Pickers
{
    public class PickerAppService : IPickerAppService
    {
        private readonly IPickerService _pickerService;
        private readonly IMapper _mapper;

        public PickerAppService(IPickerService pickerService, IMapper mapper)
        {
            _pickerService = pickerService;
            _mapper = mapper;
        }
        public async Task<ResultResponse> CreateAsync(PickerCreationViewModel pickerCreationViewModel)
        {
            var picker = _mapper.Map<Picker>(pickerCreationViewModel);
            var topics = _mapper.Map<IEnumerable<PickerTopic>>(pickerCreationViewModel.Topics);

            var businessResult = await _pickerService.CreateAsync(picker, topics);

            return new ResultResponse(businessResult, Operation.CREATE);
        }

        public async Task<ResultResponse> UpdateAsync(int id, PickerCreationViewModel pickerCreationViewModel)
        {
            var picker = _mapper.Map<Picker>(pickerCreationViewModel);
            var topics = _mapper.Map<IEnumerable<PickerTopic>>(pickerCreationViewModel.Topics);

            var businessResult = await _pickerService.UpdateAsync(id, picker, topics);

            return new ResultResponse(businessResult, Operation.UPDATE);
        }

        public async Task<ResultResponse> DeleteAsync(int id)
        {
            var businessResult = await _pickerService.DeleteAsync(id);

            return new ResultResponse(businessResult, Operation.DELETE);
        }

        public async Task<PickerViewModel> GetAsync(int id)
        {
            var picker = await _pickerService.GetAsync(id);

            var pickerViewModel = _mapper.Map<PickerViewModel>(picker);

            return pickerViewModel;
        }
    }
}
