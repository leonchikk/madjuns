using AutoMapper;
using Common.Core.SearchFilters.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IMapper Mapper;

        public BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }

        public Guid CurrentUserId
        {
            get
            {
                var indentity = HttpContext.User.Identity as ClaimsIdentity;
                return Guid.Parse(indentity.FindFirst("AccountId").Value);
            }
        }

        [NonAction]
        protected BaseListResponseModel<TResult> GetListResponse<TResult>(PagingRequestModel searchModel, IQueryable<TResult> entities)
        {
            var result = new BaseListResponseModel<TResult>
            {
                TotalCount = entities.Count(),
            };

            entities = PagingQuery(entities, searchModel.Page, searchModel.Limit);

            result.Data = entities.ToArray();

            return result;
        }

        [NonAction]
        protected IOrderedQueryable<TDbModel> PagingQuery<TDbModel>(IOrderedQueryable<TDbModel> dbQuery, int? page, int? limit)
        {
            if (page.HasValue && page > 1)
            {
                dbQuery = (IOrderedQueryable<TDbModel>)dbQuery.Skip((limit ?? 0) * (page.Value - 1));
            }
            if (limit.HasValue && limit > 0)
            {
                dbQuery = (IOrderedQueryable<TDbModel>)dbQuery.Take(limit.Value);
            }

            return dbQuery;
        }

        [NonAction]
        protected IQueryable<TDbModel> PagingQuery<TDbModel>(IQueryable<TDbModel> dbQuery, int? page, int? limit)
        {
            if (page.HasValue && page > 1)
            {
                dbQuery = dbQuery.Skip((limit ?? 0) * (page.Value - 1));
            }
            if (limit.HasValue && limit > 0)
            {
                dbQuery = dbQuery.Take(limit.Value);
            }

            return dbQuery;
        }
    }
}