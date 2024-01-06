using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    public class ProductController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (KetanDBEntities dbContext = new KetanDBEntities()) // Assuming KetanDBEntities is your DbContext
            {
                var product = dbContext.Products.ToList();
                if (product.Count==0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (KetanDBEntities dbContext = new KetanDBEntities()) // Assuming KetanDBEntities is your DbContext
            {
                var product = dbContext.Products.Find(id);
                if (product == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
        }
        [HttpPost]
        public HttpResponseMessage Post(Product product)
        {
            using (KetanDBEntities dbContext = new KetanDBEntities()) // Assuming KetanDBEntities is your DbContext
            {
                try
                {
                    dbContext.Products.Add(product);
                    dbContext.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.Created, product);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }

        public HttpResponseMessage Put(int id, Product product)
        {
            using (KetanDBEntities dbContext = new KetanDBEntities()) // Assuming KetanDBEntities is your DbContext
            {
                var existingProduct = dbContext.Products.Find(id);
                if (existingProduct == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product not found");
                }

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;

                dbContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, existingProduct);
            }
        }


        public HttpResponseMessage Delete(int id)
        {
            using (KetanDBEntities dbContext = new KetanDBEntities()) // Assuming KetanDBEntities is your DbContext
            {
                var existingProduct = dbContext.Products.Find(id);
                if (existingProduct == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product not found");
                }

                dbContext.Products.Remove(existingProduct);
                dbContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Product deleted");
            }
        }


    }
}
