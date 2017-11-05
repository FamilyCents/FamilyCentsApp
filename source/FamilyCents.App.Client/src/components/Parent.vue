<template>
  <div class="parent">
      <div>
        <v-layout row wrap>
            <v-flex
              xs4
              v-for="(child, index) in children"
              :key="index"
            >
              <v-card >
                <router-link :to="`/account/${child.customerId}`" >{{child.name}}</router-link>:
                <p>Balance: {{child.virtualBalance}}</p>
                <p>Credit Remaining: {{creditRemaining(child)}}</p>
                <p>Credit Score: {{child.virtualCreditScore}}</p>
              </v-card>
            </v-flex>
          </v-layout>
    </div>
    <TaskList></TaskList>
    
    <v-btn
      color="green"
      dark
      fixed
      right
      bottom
      fab
      @click.native.stop="addNewDialog = true"
    >
      <v-icon>add</v-icon>
    </v-btn>
    <v-dialog absolute v-model="addNewDialog">
      <v-card>
        <v-card-text>
          <v-text-field label="Task Description" v-model="newTaskDescription"></v-text-field>
          <v-text-field label="Reward" type="number" v-model="newTaskValue"></v-text-field>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn flat color="primary" @click.native="createTask">Submit</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import TaskList from './TaskList'
export default {
  name: 'Parent',
  data() {
    return {
      addNewDialog: false,
      newTaskDescription: "",
      newTaskValue: 0
    }
  },
  components: {
    TaskList
  },
  computed: {
    children() {
      return this.$store.getters.family.filter(m => !m.IsPrimary);
    }
  },
  methods: {
    creditRemaining(user){
      return user.virtualCreditLimit - user.virtualBalance;
    },
    createTask(){
      this.$store.dispatch('createFamilyTask', {
          description: this.newTaskDescription,
          value: this.newTaskValue
        });
      this.addNewDialog = false;
      this.newTaskDescription = "";
      this.newTaskValue = 0;
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>