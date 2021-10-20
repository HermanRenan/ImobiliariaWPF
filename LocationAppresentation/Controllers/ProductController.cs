using Application.Interfaces;
using Application.Model;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationAppresentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly InterfaceAppProduct _InterfaceProductApp;
        public ProductController(InterfaceAppProduct _InterfaceProductApp)
        {
            this._InterfaceProductApp = _InterfaceProductApp;
        }
        public async Task<IActionResult> Index()
        {
            var getListProduct = await _InterfaceProductApp.List();
            return View(getListProduct);
        }
        
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _InterfaceProductApp.GetEntityById((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Price,Rented,Id,Name,Theme")] ClientModel product)
        {
            if (ModelState.IsValid)
            {
                await _InterfaceProductApp.Add(product);

                if(product.Notitycoes.Any())
                {
                    foreach (var item in product.Notitycoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }

                    return View("Create", product);
                }

                
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _InterfaceProductApp.GetEntityById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Price,Rented,Id,Name,Theme")] ClientModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _InterfaceProductApp.Update(product);

                    if (product.Notitycoes.Any())
                    {
                        foreach (var item in product.Notitycoes)
                        {
                            ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                        }

                        return View("Edit", product);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _InterfaceProductApp.GetEntityById((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _InterfaceProductApp.GetEntityById(id);
            await _InterfaceProductApp.Delete(product);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            var objeto = await _InterfaceProductApp.GetEntityById(id);

            return objeto != null;
        }
    }
}
