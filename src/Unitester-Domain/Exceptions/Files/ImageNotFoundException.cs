using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unitester_Domain.Exceptions.Files;

public class ImageNotFoundException:NotFoundException
{
	public ImageNotFoundException()
	{
		this.TitleMessage = "Image not found!";
	}
}
