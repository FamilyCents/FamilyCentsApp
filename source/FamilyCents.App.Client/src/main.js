// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import Vuetify from 'vuetify'
import { store } from './store'


Vue.config.productionTip = false
Vue.use(Vuetify)
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: { App },
  store
})

// // Import top level component
// import 'bootstrap/dist/css/bootstrap.css'
// import 'element-ui/lib/theme-chalk/index.css'
import 'material-design-icons/iconfont/material-icons.css'
import 'vuetify/dist/vuetify.min.css'