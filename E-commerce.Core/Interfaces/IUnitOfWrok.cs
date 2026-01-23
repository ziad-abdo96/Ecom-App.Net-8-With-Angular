namespace E_commerce.Core.Interfaces
{
	public interface IUnitOfWrok
	{
		public ICategoryRepository CategoryRepository { get; }
		public IProductRepository ProductRepository { get; }
		public IPhotoRepository PhotoRepository { get; }
	}
}
