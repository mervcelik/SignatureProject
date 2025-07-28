using Core.CrossCuttingConcerns.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signify.WPF.Extensions;

public static class RNH
{
    public static T GetOrThrow<T>(ResponseDto<T> responseDto)
    {
        if (responseDto == null)
            throw new ArgumentNullException(nameof(responseDto), "Yanıt nesnesi boş olamaz.");

        if (responseDto.Errors != null && responseDto.Errors.Any())
        {
            string errorMessage = string.Join(Environment.NewLine, responseDto.Errors);
            throw new ApplicationException($"İşlem sırasında hata oluştu:\n{errorMessage}");
        }

        return responseDto.Data;
    }
}
