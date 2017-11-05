<template>
  <div class="task-list">
    <v-container>
      <v-list two-line subheader>
        <template v-for="(task, index) in taskList" >
          <v-list-tile :key="index">
            <v-list-tile-content>
              <v-list-tile-title>{{task.description}} - ${{task.value}}</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
          <v-divider v-if="index + 1 < taskList.length" :key="index"></v-divider>
        </template>
      </v-list>
    </v-container>
    <v-container v-if="isParent">
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
    </v-container>
  </div>
</template>

<script>


export default {
  name: 'RecentTransactions',
  data() {
    return {
      addNewDialog: false,
      newTaskDescription: "",
      newTaskValue: 0
    }
  },
  computed: {
    taskList() {
      console.log(`task list: ${this.$store.getters.tasks}`);
      return this.$store.getters.tasks.filter(t => !(t.approvedBy && t.completedBy));
    },
    isParent: function(){
      let currentUser = this.$store.getters.currentUser;      
      if(currentUser){
        console.log(`Is Parent called: ${this.$store.getters.currentUser.isPrimary}`);
        return this.$store.getters.currentUser.isPrimary;
      }
      return false;
    }
  },
  methods: {
    completeTask(taskId){
      // let taskId = event.target.value;
      console.log(`TaskId: `+taskId);
      this.$store.dispatch('completeTask', taskId);
    },
    approveTask(taskId){
      console.log(`TaskId: `+taskId);
      this.$store.dispatch('approveTask', taskId);
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
};

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
