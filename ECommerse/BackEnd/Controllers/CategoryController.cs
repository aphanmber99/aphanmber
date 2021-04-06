using System.Collections.Generic;
using System.Linq;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("controller")]
    public class CategoryController: ControllerBase
    {
        private AplicationDbContext _context;
        
        public CategoryController (AplicationDbContext context){
            context =_context;
        }
        [HttpGet]
        public List<Category> GetList(){
            List<Category> result = _context.Categories.ToList();
            return result;
        }
        [HttpGet("search")]
        public IActionResult Search(string query){
            if(query == null || query =="") return BadRequest();
            List<Category> result = _context.Categories.Where(item => item.Name.Contains(query)).ToList();
            return Ok(result);
        }
        [HttpGet("{id}")]

        public Category Get(int id){
            if(id<=0) return null;
            Category result = _context.Categories.Find(id);
            return result;
        }
        [HttpDelete("{id}")]     
           public IActionResult Delete(int id)
        {    
            if(id<=0) return NotFound();
            Category obj = _context.Categories.Find(id);
            if(obj == null) return NotFound();
            _context.Remove(obj);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Category category )
        {    
            if(id<=0) return BadRequest();
            Category obj = _context.Categories.Find(id);
            if(obj == null) return BadRequest();
            
            obj.Name = category.Name;
            obj.ID = category.ID;
           
        
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost]
        public IActionResult Create(Category category )
        {    
            _context.Add(category);
            _context.SaveChanges();
            return Ok();
        }

    }
}
