﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportToolAPI.Db;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;
using ReportToolAPI.Exceptions;
using ReportToolAPI.Interfaces;

namespace ReportToolAPI.Repository;

public class ProductsRepository : BaseRepository<Product, ProductDto>, IProductsRepository
{
    public ProductsRepository(ReportContext context, IMapper mapper) : base(context, mapper)
    {
    }
}