<template>
  <div class="home">
    <div>Menu Bar</div>
    <Parent v-if="user.isParent" />
    <Child v-else/>
  </div>
</template>

<script>
import Parent from './Parent'
import Child from './Child'

export default {
  name: 'Home',
  components: {
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
