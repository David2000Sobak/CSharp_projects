import ProjectAdd from '../ProjectAdd/ProjectAdd'
import ProjectList from '../ProjectList/ProjectList'
import ProjectTask from '../ProjectTask/ProjectTask'
import React from 'react';
import styles from './TodoList.module.scss';
import classnames from 'classnames/bind'
import { Switch, Route, Link, Redirect } from "react-router-dom"
import { connect } from "react-redux";

const cx = classnames.bind(styles)

const mapStateToProps = (state) => ({
    projects: state.projectsById.projects,
    tasks: state.tasksById.tasks,
    theme: state.theme.theme,
  })

class TodoListComponent extends React.Component {
    render() {
        return (
            <div className={cx("container")}>
                <Switch>
                    <Route path="/">
                    <div className={cx("project_container")}>
                        <Link to="/projects" style={{ textDecoration: 'none' }}><div className={cx("title", `title-theme-${this.props.theme}`)}>Projects</div></Link>
                        <Route path="/projects">
                                <ProjectAdd />
                                <ProjectList projectsById={this.props.projects} />
                        </Route>
                        </div>
                    </Route>
                </Switch>
                <Switch>
                    <Route path='/projects/:projectId/'>
                        <div className={cx("tasks_container")}>
                        <ProjectTask 
                            projectsById={this.props.projects}
                            tasksById={this.props.tasks}
                        />
                        </div>
                    </Route>
                    <Redirect to='/' />
                </Switch>
            </div>
        )
    }
}

const TodoList = connect(mapStateToProps)(TodoListComponent)

export default TodoList;