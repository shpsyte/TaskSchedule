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

function padToTwo(number) {
  if (number <= 9999) {
    number = ('0' + number).slice(-2)
  }
  return number
}

function atualizaContador() {
  var hoje = new Date()
  var futuro = new Date(document.getElementById('DateOfTest').value)
  document.getElementById('contador').innerHTML = futuro

  var ss = parseInt((futuro - hoje) / 1000)
  var mm = parseInt(ss / 60)
  var hh = parseInt(mm / 60)
  var dd = parseInt(hh / 24)
  ss = ss - mm * 60
  mm = mm - hh * 60
  hh = hh - dd * 24
  var faltam = ''
  faltam += dd && dd > 1 ? dd + 'd, ' : padToTwo(dd) == 1 ? 'd, ' : ''
  faltam += toString(hh).length ? padToTwo(hh) + ':' : ''
  faltam += toString(mm).length ? padToTwo(mm) + ':' : ''
  faltam += padToTwo(ss) + ''
  var isControlado = document.getElementById('IsHourControl').value
  var btnRealiza = document.getElementById('btbRealizarTarefa')

  if (dd + hh + mm + ss > 0) {
    document.getElementById('contador').innerHTML = faltam
    setTimeout(atualizaContador, 1000)

    if (isControlado === 'False' || (dd === 0 && hh === 0 && mm <= 04)) {
      btnRealiza.classList.remove('disabled')
    }
  } else {
    document.getElementById('contador').innerHTML = 'Realize a tarefa..'
    btnRealiza.classList.remove('disabled')
    setTimeout(atualizaContador, 1000)
  }
}

function openWindows(a) {
  url = a.dataset.url
  disableParentWin()
  var win = window.open(
    url,
    'TDCO',
    'width=1024,height=768,left=150,top=400,toolbar=0,status=0,'
  )
  win.focus()
  checkPopUpClosed(win)
}

function checkPopUpClosed(win) {
  //  var timer = setInterval(function() {
  //       if(win.closed) {
  //           clearInterval(timer);
  // enableParentWin();
  //       }
  //   }, 1000);
}
/*Function to enable parent window*/
function enableParentWin() {
  // window.document.getElementById('main').class="";
}
/*Function to enable parent window*/
function disableParentWin() {
  //  window.document.getElementById('main').class="disableWin";
}
