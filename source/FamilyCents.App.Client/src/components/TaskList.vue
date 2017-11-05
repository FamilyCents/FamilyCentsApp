<template>
  <div class="task-list">
    <v-container class="pa-1">
      <v-list two-line subheader>
        <template v-for="(task, index) in taskList" >
          <v-list-tile :key="index">
            <v-list-tile-action>
              <v-checkbox 
              v-model="task.completedBy"
              @click="completeTask(task.id)"
              v-if="!isParent"
              ></v-checkbox>
              <v-checkbox 
              v-model="task.approvedBy"
              @click="approveTask(task.id)"
              v-if="isParent"></v-checkbox>
            </v-list-tile-action>
            <v-spacer></v-spacer>
            <v-list-tile-content>
              <v-list-tile-title>{{task.description}}</v-list-tile-title>
            </v-list-tile-content>
            <v-spacer></v-spacer>
            <v-list-tile-content>
              <v-list-tile-title class="green--text text--darken-4">${{task.value}}</v-list-tile-title>
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
  name: 'TaskList',
  data() {
    return {
      addNewDialog: false,
      newTaskDescription: "",
      newTaskValue: 0
    }
  },
  computed: {
    taskList() {
      return this.$store.getters.tasks.filter(t => !(t.approvedBy && t.completedBy));
    },
    isParent: function(){
      let currentUser = this.$store.getters.currentUser;      
      if(currentUser){
        return this.$store.getters.currentUser.isPrimary;
      }
      return false;
    }
  },
  methods: {
    completeTask(taskId){
      this.$store.dispatch('completeTask', taskId);
    },
    approveTask(taskId){
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
