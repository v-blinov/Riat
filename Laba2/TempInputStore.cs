using Laba1;
using Laba1.Models;

namespace Laba2
{
    
    public class TempInputStore
    {
        private InputModel _model = new InputModel();

        public void SaveInput(InputModel input)
        {
            _model = input;
        }

        public OutputModel CalculateAndGet()
        {
            return Program.GetOutputModel(_model);
        }

        public void Stop()
        {
            _model = new InputModel();
        }
    }
}
