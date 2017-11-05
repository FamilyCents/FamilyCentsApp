import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import TaskList from '@/components/TaskList'
import AccountAdmin from '@/components/AccountAdmin'


Vue.use(Router)

export default new Router({
  mode:'hash',
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/account/:id',
      name: 'AccountAdmin',
      component: AccountAdmin
    },
    {
      path: '/tasks',
      name: 'TaskList',
      component: TaskList
    }
  ]
})
