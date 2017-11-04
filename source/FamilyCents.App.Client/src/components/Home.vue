<template>
  <v-container fluid class="home pa-0">
     <!-- <app-header></app-header> -->

    <!-- Left side column. contains the sidebar -->
    <!-- <app-sidebar></app-sidebar> -->

    <!-- Content Wrapper. Contains page content -->
    <!-- <router-view></router-view> -->
    <!-- /.content-wrapper -->

    <!-- <app-footer></app-footer> -->

    <!-- <control-sidebar></control-sidebar> -->

    
    <Parent v-if="user.isParent" />
    <Child v-else/>
  </v-container>
</template>

<script>
import Parent from './Parent'
import Child from './Child'
import Sidebar from './partials/Sidebar.vue'
import Header from './partials/Header.vue'
import Footer from './partials/Footer.vue'

export default {
  name: 'Home',
  components: {
    'app-sidebar': Sidebar,
    'app-header': Header,
    'app-footer': Footer,
    Parent,
    Child
  },
  data () {
    return {
      user: {isParent: false, name: "Charlie Brown"}
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #42b983;
}
</style>
