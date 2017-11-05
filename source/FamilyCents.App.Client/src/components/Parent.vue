<template>
  <div class="parent">
      <div>
        <ul>
          <li v-for="(child, index) in children" :key="index">
            <router-link :to="`/account/${child.customerId}`" >{{child.name}}</router-link>      
          </li>
        </ul>
    </div>
    <!-- <TaskList></TaskList> -->
  </div>
</template>

<script>

export default {
  name: 'Parent',

  // data () {
  //   return {
  //   }
  // },
  computed: {
    children() {
      return this.$store.getters.family.filter(m => !m.IsPrimary);
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>