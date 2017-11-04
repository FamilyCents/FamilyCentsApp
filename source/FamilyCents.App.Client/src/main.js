// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import ElementUI from 'element-ui'
import App from './App'
import router from './router'
import Vuetify from 'vuetify'
Vue.use(Vuetify)

Vue.config.productionTip = false
Vue.use(ElementUI)
Vue.use(ElementUI, {locale: 'en'})

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: { App }
})

// // Import top level component
// import 'bootstrap/dist/css/bootstrap.css'
import 'element-ui/lib/theme-chalk/index.css'
import 'material-design-icons/iconfont/material-icons.css'
import 'vuetify/dist/vuetify.min.css'