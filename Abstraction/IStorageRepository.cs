using WebApp_vSem3.DTO;

namespace WebApp_vSem3.Abstraction
{
    public interface IStorageRepository
    {
        IEnumerable<StorageModel> GetAllStorages();
        int AddStorage(StorageModel storageModel);
        void DeleteStorage(int id);
    }
}
