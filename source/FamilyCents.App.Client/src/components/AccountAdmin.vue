<template>
  <div class="parent">
    <!-- <div>
      {{$route.params.id}}
    </div> -->
    <v-layout row wrap>
      <v-flex xs12 class="pa-2">
          <v-avatar size="80px" class="grey lighten-4">
              <img src="/static/avatar-default.png" alt="avatar">
          </v-avatar>
      </v-flex>
      <v-flex xs9>
        <v-slider label="Minimum Credit Line" v-bind:max="1000" v-model="minCredit"></v-slider>
      </v-flex>
      <v-flex xs3>
        <v-text-field v-model="minCredit" type="number"></v-text-field>
      </v-flex>
      <v-flex xs9>
        <v-slider label="Maximum Credit Line" v-bind:max="1000" v-model="maxCredit"></v-slider>
      </v-flex>
      <v-flex xs3>
        <v-text-field v-model="maxCredit" type="number"></v-text-field>
      </v-flex>
    </v-layout>
    <!-- <TaskList></TaskList> -->
  </div>
</template>

<script>

export default {
  name: 'Parent',
  data () {
    return {
      minCredit: 0,
      maxCredit: 0
    }
  },
  methods: {
    updateCreditRange: function(){
      
    }
  },
  created() {
    //TODO: fetch user from api

  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>