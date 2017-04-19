import axios from 'axios'
import config from '../config'
import router from '../router'
import store from '../store'

export default {
  request (method, uri, data = null, headers = {}) {
    if (!method) {
      console.error('API function call requires method argument')
      return
    }

    if (!uri) {
      console.error('API function call requires uri argument')
      return
    }

    if (uri !== '/authentication') {
      this.checkAuthorization()
    }

    var url = config.serverURI + uri
    headers = Object.assign(headers, {Authorization: store.state.token})

    return axios({method, url, data, headers: headers})
  },
  checkAuthorization () {
    axios({method: 'get', url: config.serverURI + '/authentication', data: null, headers: {Authorization: store.state.token}})
      .then(response => {
        var data = response.data

        /* Setting user in the state and caching record to the localStorage */
        if (data.id_token) {
          var token = data.token_type + ' ' + data.id_token

          store.commit('SET_TOKEN', token)
          if (window.localStorage) {
            window.localStorage.setItem('token', token)
          }
        }
      })
      .catch(() => {
        store.commit('SET_USER', 'null')
        store.commit('SET_TOKEN', 'null')

        if (window.localStorage) {
          window.localStorage.removeItem('user')
          window.localStorage.removeItem('token')
        }

        router.go('/login')
      })
  }
}
