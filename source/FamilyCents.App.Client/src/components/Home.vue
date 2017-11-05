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

    
    <Parent v-if="isParent && !isLoading" />
    <Child v-else-if="!isParent && !isLoading" />
    <div v-else>Loading...</div>
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
  computed: {
    isParent: function(){
      let currentUser = this.$store.getters.currentUser;
      console.log(`Is Parent called 1: ${this.$store.getters.currentUser}`);
      
      if(currentUser){
        console.log(`Is Parent called: ${this.$store.getters.currentUser.isPrimary}`);
        return this.$store.getters.currentUser.isPrimary;
      }
      return false;
    },
    isLoading: function(){
      console.log( `Loading: ${this.$store.getters.isLoading}`);
      return this.$store.getters.isLoading;
    }
  },
  beforeCreate(){
    this.$store.dispatch('initializeFamily', [123200000, 123210000]);
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
