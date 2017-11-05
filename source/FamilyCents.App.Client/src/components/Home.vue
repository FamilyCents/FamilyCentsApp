<template>
  <v-container fluid class="home pa-0">    
    <Parent v-if="isParent && !isLoading" />
    <Child v-else-if="!isParent && !isLoading" :user="currentUser"/>
    <v-progress-linear v-bind:indeterminate="true" v-else></v-progress-linear>
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
    currentUser: function(){
      return this.$store.getters.currentUser;
    },
    isParent: function(){
      let currentUser = this.$store.getters.currentUser;      
      if(currentUser){
        return this.$store.getters.currentUser.isPrimary;
      }
      return false;
    },
    isLoading: function(){
      return this.$store.getters.isLoading;
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
