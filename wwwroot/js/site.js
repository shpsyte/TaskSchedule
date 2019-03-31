// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SearchCep(_cep) {
  _cep = _cep.replace(/\D/g, '')
  if (_cep != '') {
    cep(_cep).then(a => {
      document.querySelector('#CityAndState').value = ` ${a.city}-${a.state}`
      document.querySelector('#Neighborhood').value = ` ${a.neighborhood}`
      document.querySelector('#Address').value = ` ${a.street}`
      document.querySelector('#Number').focus()
    })
  }
}
