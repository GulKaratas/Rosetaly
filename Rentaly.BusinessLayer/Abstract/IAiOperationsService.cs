using Rentaly.DtoLayer.RentalDtos;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface IAiOperationsService
    {
        Task<string> BuildMorningBriefingAsync();
        Task<List<PendingRentalAnalysisDto>> BuildPendingAnalysesAsync();
    }
}
