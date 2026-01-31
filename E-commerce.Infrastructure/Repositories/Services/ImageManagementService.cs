using E_commerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace E_commerce.Infrastructure.Repositories.Services
{
	public class ImageManagementService : IImageManagmentService
	{
		private readonly IFileProvider _fileProvider;

		public ImageManagementService(IFileProvider fileProvider)
		{
			_fileProvider = fileProvider;
		}

		public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
		{
			var saveImageSrc = new List<string>();
			var imageDirectory = Path.Combine("wwwroot", "Images", src);
			if(Directory.Exists(imageDirectory) is not true)
			{
				Directory.CreateDirectory(imageDirectory);
			}

			foreach(var file in files)
			{
				if(file.Length > 0)
				{
					var imageName = file.Name;
					var imageSrc = $"Images/{src}/{imageName}";
					var root = Path.Combine(imageDirectory, imageName);
					using(FileStream stream = new FileStream(root, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}
					saveImageSrc.Add(imageName);
				}
			}

			return saveImageSrc;
		}

		public void DeleteImageAsync(string src)
		{
			var info = _fileProvider.GetFileInfo(src);
			var root = info.PhysicalPath;
			File.Delete(root);
		}
	}
}
