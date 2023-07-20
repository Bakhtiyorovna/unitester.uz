﻿namespace Unitester_Domain.Exceptions.Files;

public class ImageNotFoundException : NotFoundException
{
    public ImageNotFoundException()
    {
        this.TitleMessage = "Image not found!";
    }
}
